export class AiSearchInterop {
    constructor() {
        this.modules = new Map();
    }

    initialize(scriptUrl) {
        if (!scriptUrl) {
            throw new Error("A Cloudflare AI Search snippet script URL is required.");
        }

        let modulePromise = this.modules.get(scriptUrl);

        if (!modulePromise) {
            modulePromise = import(scriptUrl);
            this.modules.set(scriptUrl, modulePromise);
        }

        return modulePromise;
    }
}

window.AiSearchInterop = new AiSearchInterop();
