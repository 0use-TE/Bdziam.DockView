// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

/**
 * Event handler utility for managing DOM events
 */

// Storage for event handlers
const eventRegistry = {};
let uidEvent = 1;

// Custom event mappings
const customEvents = {
    mouseenter: 'mouseover',
    mouseleave: 'mouseout'
};

// Standard DOM events
const nativeEvents = new Set([
    'click', 'dblclick', 'mouseup', 'mousedown', 'contextmenu', 'mousewheel',
    'DOMMouseScroll', 'mouseover', 'mouseout', 'mousemove', 'selectstart',
    'selectend', 'keydown', 'keypress', 'keyup', 'orientationchange',
    'touchstart', 'touchmove', 'touchend', 'touchcancel', 'pointerdown',
    'pointermove', 'pointerup', 'pointerleave', 'pointercancel',
    'gesturestart', 'gesturechange', 'gestureend', 'focus', 'blur',
    'change', 'reset', 'select', 'submit', 'focusin', 'focusout',
    'load', 'unload', 'beforeunload', 'resize', 'move', 'DOMContentLoaded',
    'readystatechange', 'error', 'abort', 'scroll'
]);

/**
 * Private helper functions
 */
function makeEventUid(element, uid) {
    return (uid && `${uid}::${uidEvent++}`) || element.uidEvent || uidEvent++;
}

function getElementEvents(element) {
    const uid = makeEventUid(element);
    element.uidEvent = uid;
    eventRegistry[uid] = eventRegistry[uid] || {};
    return eventRegistry[uid];
}

function createHandler(element, fn) {
    return function handler(event) {
        // Add delegateTarget property to event
        Object.defineProperty(event, 'delegateTarget', {
            value: element,
            writable: false
        });

        if (handler.oneOff) {
            EventHandler.off(element, event.type, fn);
        }

        return fn.apply(element, [event]);
    };
}

function createDelegationHandler(element, selector, fn) {
    return function handler(event) {
        const domElements = element.querySelectorAll(selector);

        for (let { target } = event; target && target !== this; target = target.parentNode) {
            for (const domElement of domElements) {
                if (domElement !== target) {
                    continue;
                }

                Object.defineProperty(event, 'delegateTarget', {
                    value: target,
                    writable: false
                });

                if (handler.oneOff) {
                    EventHandler.off(element, event.type, selector, fn);
                }

                return fn.apply(target, [event]);
            }
        }
    };
}

function findHandler(events, callable, delegationSelector = null) {
    return Object.values(events)
        .find(event => event.callable === callable && event.delegationSelector === delegationSelector);
}

function normalizeParameters(originalTypeEvent, handler, delegationFunction) {
    const isDelegated = typeof handler === 'string';
    const callable = isDelegated ? delegationFunction : (handler || delegationFunction);
    let typeEvent = getTypeEvent(originalTypeEvent);

    if (!nativeEvents.has(typeEvent)) {
        typeEvent = originalTypeEvent;
    }

    return [isDelegated, callable, typeEvent];
}

function getTypeEvent(event) {
    // Remove namespace from event name
    event = event.replace(/\..*/, '');
    return customEvents[event] || event;
}

function addHandler(element, originalTypeEvent, handler, delegationFunction, oneOff) {
    if (typeof originalTypeEvent !== 'string' || !element) {
        return;
    }

    let [isDelegated, callable, typeEvent] = normalizeParameters(originalTypeEvent, handler, delegationFunction);

    // Handle custom events like mouseenter/mouseleave
    if (originalTypeEvent in customEvents) {
        const wrapFunction = fn => {
            return function (event) {
                if (!event.relatedTarget ||
                    (event.relatedTarget !== event.delegateTarget &&
                     !event.delegateTarget.contains(event.relatedTarget))) {
                    return fn.call(this, event);
                }
            };
        };
        callable = wrapFunction(callable);
    }

    const events = getElementEvents(element);
    const handlers = events[typeEvent] || (events[typeEvent] = {});
    const previousFunction = findHandler(handlers, callable, isDelegated ? handler : null);

    if (previousFunction) {
        previousFunction.oneOff = previousFunction.oneOff && oneOff;
        return;
    }

    const uid = makeEventUid(callable, originalTypeEvent.replace(/[^.]*(?=\..*)\.|.*/, ''));
    const fn = isDelegated ?
        createDelegationHandler(element, handler, callable) :
        createHandler(element, callable);

    fn.delegationSelector = isDelegated ? handler : null;
    fn.callable = callable;
    fn.oneOff = oneOff;
    fn.uidEvent = uid;
    handlers[uid] = fn;

    element.addEventListener(typeEvent, fn, isDelegated);
}

function removeHandler(element, events, typeEvent, handler, delegationSelector) {
    const fn = findHandler(events[typeEvent], handler, delegationSelector);

    if (!fn) {
        return;
    }

    element.removeEventListener(typeEvent, fn, Boolean(delegationSelector));
    delete events[typeEvent][fn.uidEvent];
}

/**
 * Main EventHandler object
 */
const EventHandler = {
    /**
     * Adds an event listener
     * @param {HTMLElement} element - The target element
     * @param {string} event - The event type
     * @param {string|Function} handler - The event handler or selector for delegation
     * @param {Function} delegationFunction - The delegation function (when handler is a selector)
     */
    on(element, event, handler, delegationFunction) {
        addHandler(element, event, handler, delegationFunction, false);
    },

    /**
     * Adds a one-time event listener
     * @param {HTMLElement} element - The target element
     * @param {string} event - The event type
     * @param {string|Function} handler - The event handler or selector for delegation
     * @param {Function} delegationFunction - The delegation function (when handler is a selector)
     */
    one(element, event, handler, delegationFunction) {
        addHandler(element, event, handler, delegationFunction, true);
    },

    /**
     * Removes event listeners
     * @param {HTMLElement} element - The target element
     * @param {string} originalTypeEvent - The event type
     * @param {string|Function} handler - The event handler or selector
     * @param {Function} delegationFunction - The delegation function
     */
    off(element, originalTypeEvent, handler, delegationFunction) {
        if (typeof originalTypeEvent !== 'string' || !element) {
            return;
        }

        const [isDelegated, callable, typeEvent] = normalizeParameters(originalTypeEvent, handler, delegationFunction);
        const inNamespace = typeEvent !== originalTypeEvent;
        const events = getElementEvents(element);
        const storeElementEvent = events[typeEvent] || {};
        const isNamespace = originalTypeEvent.startsWith('.');

        if (typeof callable !== 'undefined') {
            if (!Object.keys(storeElementEvent).length) {
                return;
            }
            removeHandler(element, events, typeEvent, callable, isDelegated ? handler : null);
            return;
        }

        if (isNamespace) {
            for (const elementEvent of Object.keys(events)) {
                const namespace = originalTypeEvent.slice(1);
                for (const handlerKey of Object.keys(events[elementEvent])) {
                    if (handlerKey.includes(namespace)) {
                        const event = events[elementEvent][handlerKey];
                        removeHandler(element, events, elementEvent, event.callable, event.delegationSelector);
                    }
                }
            }
        }

        for (const keyHandlers of Object.keys(storeElementEvent)) {
            const handlerKey = keyHandlers.replace(/::\d+$/, '');

            if (!inNamespace || originalTypeEvent.includes(handlerKey)) {
                const event = storeElementEvent[keyHandlers];
                removeHandler(element, events, typeEvent, event.callable, event.delegationSelector);
            }
        }
    },

    /**
     * Triggers an event
     * @param {HTMLElement} element - The target element
     * @param {string} event - The event type
     * @param {object} args - Event arguments/data
     * @returns {Event} The dispatched event
     */
    trigger(element, event, args) {
        if (typeof event !== 'string' || !element) {
            return null;
        }

        const typeEvent = getTypeEvent(event);
        let evt = new Event(event, { bubbles: true, cancelable: true });

        // Add custom properties to the event
        if (args) {
            for (const [key, value] of Object.entries(args)) {
                try {
                    evt[key] = value;
                } catch {
                    Object.defineProperty(evt, key, {
                        configurable: true,
                        get() {
                            return value;
                        }
                    });
                }
            }
        }

        element.dispatchEvent(evt);
        return evt;
    }
};

export default EventHandler;
