window.toCSString = function(data) {
    var bufferSize = lengthBytesUTF8(data) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(data, buffer, bufferSize);
    return buffer;
}

window.toJSString = function(data) {
    return UTF8ToString(data);
}