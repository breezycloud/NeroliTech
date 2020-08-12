function saveAsFile(fileName, bytesBase64) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:application/octet-stream;base64,' + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function pdfViewer() {
    const viewerElement = document.getElementById('viewer');
    WebViewer({
        path: 'https://pdftron.s3.amazonaws.com/webviewer/5.1.0/lib',
        initialDoc: 'https://pdftron.s3.amazonaws.com/downloads/pl/webviewer-demo.pdf', // replace with your own PDF file
    }, viewerElement).then((instance) => {
        // call apis here
    });
}
