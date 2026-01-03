mergeInto(LibraryManager.library, {
  GetLanguage: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },
  SetLeaderboard: function (value)
  {
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('Score', value);
    });
  },
  ReviveAd: function ()
  {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onRewarded: () => {
          myGameInstance.SendMessage('Management', 'Revive');
        }
      }
    })
  },
  FullscreenAd: function ()
  {
    ysdk.adv.showFullscreenAdv({callbacks:{}});
  }
});