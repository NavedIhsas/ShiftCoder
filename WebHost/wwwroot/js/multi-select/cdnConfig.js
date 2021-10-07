


class MyUploadAdapter {
    constructor(loader) {
        debugger;
        // The file loader instance to use during the upload.
        this.loader = loader;
    }
    
    // Starts the upload process.
    upload() {
        debugger;
        return this.loader.file
            .then(file => new Promise((resolve, reject) => {
                this._initRequest();
                this._initListeners(resolve, reject, file);
                this._sendRequest(file);
            }));

    }

    // Aborts the upload process.
    abort() {
        // Reject the promise returned from the upload() method.
        server.abortUpload();
    }
  }

function MyCustomUploadAdapterPlugin(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
        loader.filebrowserImageUploadUrl = "Index?handler=UploadImage";
        debugger;
        return new MyUploadAdapter(loader);
    };
}