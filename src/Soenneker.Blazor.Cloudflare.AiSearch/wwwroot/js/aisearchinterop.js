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

    async configureSearchBar(searchBar, hideSubmitButton) {
        if (!searchBar) {
            throw new Error("A rendered Cloudflare AI Search bar is required.");
        }

        await customElements.whenDefined("search-bar-snippet");

        if (!searchBar.shadowRoot) {
            throw new Error("The Cloudflare AI Search bar shadow root is unavailable.");
        }

        const styleSelector = "style[data-soenneker-hide-submit-button]";
        const existingStyle = searchBar.shadowRoot.querySelector(styleSelector);

        if (!hideSubmitButton) {
            existingStyle?.remove();
            return;
        }

        if (existingStyle) {
            return;
        }

        const style = document.createElement("style");
        style.dataset.soennekerHideSubmitButton = "";
        style.textContent = ".search-submit-button { display: none !important; }";
        searchBar.shadowRoot.appendChild(style);
    }
}

window.AiSearchInterop = new AiSearchInterop();
