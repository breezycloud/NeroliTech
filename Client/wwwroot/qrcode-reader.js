function decodeBarcode(dotNetObject) {
    var codeReader = new ZXing.BrowserQRCodeReader()
    document.getElementById('result').textContent = 'Code reader initialized'    
    codeReader.decodeFromInputVideoDevice(undefined, 'video').then((result) => {
        var content = result.text
        console.log(result.text)
        document.getElementById('result').textContent = 'Success'
        dotNetObject.invokeMethodAsync('LoadItem', content);
    }).catch((err) => {
        console.error(err)
        document.getElementById('result').textContent = err
    })
    //codeReader.decodeFromInputVideoDeviceContinuously(undefined, 'video', (result, err) => {
    //    if (result) {
    //        document.getElementById('result').textContent = 'Success'
    //        dotNetObject.invokeMethodAsync('LoadItem', result.text);
    //        codeReader.stopContinuousDecode()    
    //    }
    //    if (err) {
    //        if (err instanceof ZXing.NotFoundException) {
    //            document.getElementById('result').textContent = 'Scanning...'
    //        }
    //        if (err instanceof ZXing.FormatException) {
    //            document.getElementById('result').textContent = 'Try again'
    //        }
    //    }
    //})
}

function testing() {

    console.log('Testing!!');
}