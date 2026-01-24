mergeInto(LibraryManager.library, {

  FullscreenAd: function () {
    ysdk.adv.showFullscreenAdv();
  },

  ReviveAd: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onClose: () => {
          gameInstance.SendMessage('Management', 'OnReviveFinished');
        },
        onRewarded: () => {
          gameInstance.SendMessage('Management', 'OnReviveCompleted');
        }
      }
    })
  },

  IsAuthorized: function () {
    return player && player.isAuthorized();
  },

  GetLanguage: function () {
    return window.toCSString(ysdk.environment.i18n.lang);
  },

  GetName: function () {
    return window.toCSString(player.getName());
  },

  GetAvatar: function () {
    return window.toCSString(player.getPhoto("medium"));
  },

  SetLeaderboard: function (value) {
    ysdk.leaderboards.setScore('Score', value);
  },

  SaveData: function (data) {
    player.setData(JSON.parse(window.toJSString(data)));
  },

  LoadData: function () {
    player.getData().then(_data => {
      gameInstance.SendMessage('Management', 'OnDataLoaded', JSON.stringify(_data));
    });
  }

});