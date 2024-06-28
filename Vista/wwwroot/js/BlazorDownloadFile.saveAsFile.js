﻿window.guardarExcel = function (fileName, byteBase64, contentType){
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:' + contentType + ';base64,' + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

window.abrirPDF = function (dataUrl) {
    var blob = base64toBlob(dataUrl);
    var url = URL.createObjectURL(blob);
    window.open(url, '_blank');
}

function base64toBlob(base64) {
    var byteString = atob(base64);
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);

    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: 'application/pdf' });
}