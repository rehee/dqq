using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.ChapterServices
{
	public interface IChapterService
	{
		Task<ContentResponse<bool>> ProcessChapter(Guid? actorId, EnumChapter? chapter = null);
	}
}
