import { BrowserQRCodeReader } from '@zxing/library';
export function getQrCodeText() {

    const codeReader = new BrowserQRCodeReader()
    console.log('ZXing code reader initialized')
    
    codeReader.decodeFromInputVideoDevice(undefined, 'video').then()
    codeReader.decodeFromInputVideoDeviceContinuously(undefined, 'video', (result, err) => {
        if (result) {
            // properly decoded qr code
            console.log('Found QR code!', result)
            document.getElementById('result').textContent = result.text
        }
        if (err) {
            // As long as this error belongs into one of the following categories
            // the code reader is going to continue as excepted. Any other error
            // will stop the decoding loop.
            //
            // Excepted Exceptions:
            //
            //  - NotFoundException
            //  - ChecksumException
            //  - FormatException

            if (err instanceof ZXing.NotFoundException) {
                console.log('No QR code found.')
            }

            if (err instanceof ZXing.ChecksumException) {

                console.log('A code was found, but it\'s read value was not valid.')
            }

            if (err instanceof ZXing.FormatException) {
                console.log('A code was found, but it was in a invalid format.')
            }
        }
    })       
}

