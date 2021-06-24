using GetInTouch.Logic.ViewModels.Appreciation;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface IAppreciationLogic
    {
        AppreciationModel CreateOrUpdateAppreciation(Guid postId, Guid userId, Emoji emoji);
        AppreciationViewModel GetPostAppreciationInfo(Guid postId, Guid userId);
    }
}
