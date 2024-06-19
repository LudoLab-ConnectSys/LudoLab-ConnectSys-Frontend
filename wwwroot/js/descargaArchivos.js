function DescargarExel(nombreArchivo, base64Archivo) {
    const link = document.createElement("a");
    link.download = nombreArchivo;
    link.href = "data:application/vnd.ms-excel;base64," + base64Archivo;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function saveAsFile(fileName, base64Data) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:application/octet-stream;base64,' + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
// wwwroot/js/interop.js
window.openExcelInNewTab = function (fileContent, contentDisposition) {
    const blob = b64toBlob(fileContent.split(",")[1]);
    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "file.xlsx";
    a.target = "_blank";
    a.rel = "noopener noreferrer";

    if (contentDisposition) {
        a.setAttribute("href", url);
        a.setAttribute("download", "file.xlsx");
        a.setAttribute("target", "_blank");
        a.setAttribute("rel", "noopener noreferrer");
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }
};

function b64toBlob(b64Data, contentType = "", sliceSize = 512) {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);
        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
}
