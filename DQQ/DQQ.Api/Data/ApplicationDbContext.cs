using DQQ.Attributes;
using DQQ.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReheeCmf.ContextModule.Contexts;
using ReheeCmf.ContextModule.Entities;
using System.Numerics;
using System.Reflection.Emit;

namespace DQQ.Api.Data
{
  public class ApplicationDbContext : CmfIdentityContext<ReheeCmfBaseUser>
  {
    public ApplicationDbContext(IServiceProvider sp) : base(sp)
    {

    }

    public DbSet<ActorEntity> ActorEntities { get; set; }
    public DbSet<SkillEntity> SkillEntities { get; set; }
    public DbSet<ItemEntity> ItemEntities { get; set; }
    public DbSet<ActorEquipmentEntity> ActorEquipmentEntities { get; set; }
		public DbSet<ActorBuild> ActorBuilds { get; set; }
	}
}
