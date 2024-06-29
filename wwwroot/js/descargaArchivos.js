function DescargarExel(nombreArchivo, base64Archivo) {
    const link = document.createElement("a");
    link.download = nombreArchivo;
    link.href = "data:application/vnd.ms-excel;base64," + base64Archivo;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function DescargarExelnuevo(nombreArchivo, base64Archivo) {
    const link = document.createElement("a");
    link.download = nombreArchivo;
    link.href = "data:application/vnd.ms-excel.sheet.macroEnabled.12;base64," + base64Archivo;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
