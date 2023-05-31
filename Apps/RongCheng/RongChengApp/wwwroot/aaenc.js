function getD() {
    var _0x5b0f21 = new Date();
    return _0x5b0f21['getFullYear']() + '-' + (_0x5b0f21['getMonth']() + 0x1) + '-' + _0x5b0f21['getDate']() + '\x20' + _0x5b0f21['getHours']() + ':00';
}
function aaaEnc(e, g) {
    debugger;
    // console.log(e);
    e = Date.parse(e);

    var c = 0;
    var b = CryptoJS.enc.Utf8.parse(e);
    var d = g;
    if (d.length > 64 * 2) {
        d = d.substr(d.length - 64 * 2)
    }
    var j = d.substr(0, 64);
    var h = d.substr(64);
    var f = new SM2Cipher(c);
    var a = f.CreatePoint(j, h);
    b = f.GetWords(b.toString());
    var i = f.Encrypt(a, b);
    return "04" + i
}
/**
es  is Arrary of string
*/

function aaaEncBat(es,g){
    let result=[];
    for(let e of es)      {
       let item= aaaEnc(e,g)
        result.push(item);
    }
    return result;
}