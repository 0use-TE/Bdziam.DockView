// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

/**
 * Data storage utility for managing component instances and data
 */
const elementMap = new Map();

const Data = {
    /**
     * Sets data for an element or key
     * @param {string|HTMLElement} element - The element or key to store data for
     * @param {any} instance - The data/instance to store
     */
    set(element, instance) {
        if (!elementMap.has(element)) {
            elementMap.set(element, instance);
        }
    },

    /**
     * Gets data for an element or key
     * @param {string|HTMLElement} element - The element or key to retrieve data for
     * @returns {any} The stored data/instance or null if not found
     */
    get(element) {
        if (elementMap.has(element)) {
            return elementMap.get(element);
        }
        return null;
    },

    /**
     * Removes data for an element or key
     * @param {string|HTMLElement} element - The element or key to remove data for
     */
    remove(element) {
        if (elementMap.has(element)) {
            elementMap.delete(element);
        }
    },

    /**
     * Checks if data exists for an element or key
     * @param {string|HTMLElement} element - The element or key to check
     * @returns {boolean} True if data exists, false otherwise
     */
    has(element) {
        return elementMap.has(element);
    },

    /**
     * Clears all stored data
     */
    clear() {
        elementMap.clear();
    },

    /**
     * Gets the size of the data store
     * @returns {number} The number of stored items
     */
    get size() {
        return elementMap.size;
    }
};

export default Data;
