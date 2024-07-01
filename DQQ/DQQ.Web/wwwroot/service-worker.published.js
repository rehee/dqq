// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

// Import the service-worker-assets.js which contains the assets manifest
self.importScripts('./service-worker-assets.js');

// Register event listeners for install, activate, and fetch events
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;

// Define patterns to include/exclude assets for caching
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/];
const offlineAssetsExclude = [/^service-worker\.js$/];

// Replace with your base path if you are hosting on a subfolder. Ensure there is a trailing '/'.
const base = "/";
const baseUrl = new URL(base, self.origin);
const manifestUrlList = self.assetsManifest.assets.map(asset => new URL(asset.url, baseUrl).href);

async function onInstall(event) {
  console.info('Service worker: Install');

  // Fetch and cache all matching items from the assets manifest
  const assetsRequests = self.assetsManifest.assets
    .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
    .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
    .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));

  // Open cache and add all filtered assets
  await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
}

async function onActivate(event) {
  console.info('Service worker: Activate');

  // Delete unused caches
  const cacheKeys = await caches.keys();
  await Promise.all(cacheKeys
    .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
    .map(key => caches.delete(key)));
}

// Handle fetch events
async function onFetch(event) {
  if (event.request.method === 'GET') {
    const cache = await caches.open(cacheName);
    try {
      // Try to fetch from network first
      const networkResponse = await fetch(event.request);

      // Check if the request URL is from the same origin
      if (event.request.url.startsWith(self.origin)) {
        // Clone and cache the response if it's from the same origin
        cache.put(event.request, networkResponse.clone());
      }

      // Return the network response
      return networkResponse;
    } catch (error) {
      // If network fetch fails, try to get from cache
      const cachedResponse = await cache.match(event.request);
      return cachedResponse || Promise.reject('no-match');
    }
  }

  // For non-GET requests, fall back to network
  return fetch(event.request);
}
