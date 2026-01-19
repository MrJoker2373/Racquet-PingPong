mergeInto(LibraryManager.library, {

  FullscreenAd: function () {
    ysdk.adv.showFullscreenAdv({callbacks:{}});
  },

  ReviveAd: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onRewarded: () => {
          gameInstance.SendMessage('Management', 'ReviveCompleted');
        }
      }
    })
  },

  SetUserData: function (data) {
    var text = UTF8ToString(data);
    var myData = JSON.parse(text);
    player.setData(myData);
  },

  GetUserData: function () {
    player.getData().then(_data => {
        return JSON.stringify(_data);
    });
  },

  SetLeaderboardScore: function (value) {
    ysdk.leaderboards.setScore('Score', value);
  },

  GetLanguage: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

  GetUserName: function () {
    return player.getName();
  },

  GetUserAvatar: function () {
    return player.getPhoto("medium");
  }

});