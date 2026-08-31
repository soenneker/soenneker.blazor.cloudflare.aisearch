const searchBarStyles = `
    .search-input-wrapper {
        min-height: 38px;
        padding: 0 11px;
        gap: var(--search-snippet-spacing-sm);
        background: var(--search-snippet-background);
    }

    .search-icon {
        width: var(--search-snippet-icon-size);
        height: var(--search-snippet-icon-size);
        flex: none;
    }

    .search-input {
        min-width: 0;
        height: 36px;
        font-weight: var(--search-snippet-font-weight-normal);
    }

    .search-view:focus-within {
        box-shadow: 0 0 0 3px var(--search-snippet-focus-ring);
    }

    .search-view:has(.search-input:not(:placeholder-shown)) .search-input-wrapper {
        border-radius: var(--search-snippet-border-radius);
    }

    .search-content,
    .search-view:has(.search-input:not(:placeholder-shown)) .search-content {
        top: calc(100% + var(--search-snippet-spacing-sm));
        right: 0;
        left: auto;
        width: min(25rem, calc(100vw - 2rem));
        max-height: min(32rem, calc(100vh - 5rem));
        padding: 0 !important;
        overflow-x: hidden;
        overflow-y: auto;
        border: var(--search-snippet-border-width) solid var(--search-snippet-border-color);
        border-radius: calc(var(--search-snippet-border-radius) + 2px);
        box-shadow: var(--search-snippet-shadow-lg);
    }

    .search-header {
        position: sticky;
        z-index: 1;
        top: 0;
        min-height: 42px;
        margin: 0;
        padding: var(--search-snippet-spacing-sm) var(--search-snippet-spacing-md);
        gap: var(--search-snippet-spacing-md);
        background: var(--search-snippet-background);
    }

    .search-count,
    .powered-by-inline {
        white-space: nowrap;
    }

    .powered-by-inline {
        min-width: 0;
        font-size: 11px;
    }

    .powered-by-inline a {
        display: inline-flex;
        align-items: center;
        gap: var(--search-snippet-spacing-xs);
    }

    .powered-by-inline svg {
        width: 25px;
        height: auto;
    }

    .search-results {
        gap: 2px;
        padding: 6px;
    }

    a.search-result-item {
        min-height: 54px;
        align-items: center;
        gap: var(--search-snippet-spacing-sm);
        padding: 8px 10px;
        background: transparent;
        border-color: transparent;
        border-radius: var(--search-snippet-border-radius);
        box-shadow: none;
    }

    a.search-result-item:hover,
    a.search-result-item:focus-visible {
        background: var(--search-snippet-hover-background);
        border-color: transparent;
        box-shadow: none;
        transform: none;
    }

    .search-result-image-container {
        width: 40px;
        height: 40px;
    }

    .search-result-title {
        margin: 0;
        font-size: var(--search-snippet-font-size-base);
        line-height: 1.35;
    }

    .search-result-snippet:empty {
        display: none;
    }

    .search-result-url {
        display: block;
        margin-top: 3px;
        overflow: hidden;
        color: var(--search-snippet-text-secondary);
        line-height: 1.3;
        text-overflow: ellipsis;
        white-space: nowrap;
    }

    .search-result-url:hover {
        text-decoration: none;
    }

    .search-loading,
    .search-empty {
        min-height: 112px;
        padding: var(--search-snippet-spacing-xl);
    }

    .error {
        min-height: 60px;
        margin: 6px;
        padding: var(--search-snippet-spacing-md);
        color: var(--search-snippet-text-color);
        background: var(--search-snippet-surface);
        border: none;
        border-radius: var(--search-snippet-border-radius);
        font-size: var(--search-snippet-font-size-sm);
        line-height: 1.4;
    }

    .error strong {
        color: var(--search-snippet-text-color);
        font-weight: var(--search-snippet-font-weight-medium);
    }

    .search-content::-webkit-scrollbar {
        width: 6px;
    }

    .search-content::-webkit-scrollbar-track {
        background: transparent;
    }
`;

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

        const presentationStyleSelector = "style[data-soenneker-search-bar]";

        if (!searchBar.shadowRoot.querySelector(presentationStyleSelector)) {
            const presentationStyle = document.createElement("style");
            presentationStyle.dataset.soennekerSearchBar = "";
            presentationStyle.textContent = searchBarStyles;
            searchBar.shadowRoot.appendChild(presentationStyle);
        }

        const submitStyleSelector = "style[data-soenneker-hide-submit-button]";
        const submitStyle = searchBar.shadowRoot.querySelector(submitStyleSelector);

        if (!hideSubmitButton) {
            submitStyle?.remove();
            return;
        }

        if (submitStyle) {
            return;
        }

        const style = document.createElement("style");
        style.dataset.soennekerHideSubmitButton = "";
        style.textContent = ".search-submit-button { display: none !important; }";
        searchBar.shadowRoot.appendChild(style);
    }
}

window.AiSearchInterop = new AiSearchInterop();
