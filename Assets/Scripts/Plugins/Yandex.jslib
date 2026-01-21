mergeInto(LibraryManager.library, {

  FullscreenAd: function () {
    ysdk.adv.showFullscreenAdv({callbacks:{}});
  },

  ReviveAd: function () {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onRewarded: () => {
          gameInstance.SendMessage('Management', 'OnReviveCompleted');
        },
        onClose: () => {
          gameInstance.SendMessage('Management', 'OnReviveFinished');
        }
      }
    })
  },

  SetUserData: function (data) {
    player.setData(JSON.parse(window.toJSString(data)));
  },

  GetUserData: function () {
    player.getData().then(_data => {
        gameInstance.SendMessage('Management', 'OnUserLoaded', JSON.stringify(_data));
    });
  },

  SetLeaderboardScore: function (value) {
    ysdk.leaderboards.setScore('Score', value);
  },

  GetLanguage: function () {
    return window.toCSString(ysdk.environment.i18n.lang);
  },

  GetUserName: function () {
    return window.toCSString(player.getName());
  },

  GetUserAvatar: function () {
    return window.toCSString(player.getPhoto("medium"));
  }

});