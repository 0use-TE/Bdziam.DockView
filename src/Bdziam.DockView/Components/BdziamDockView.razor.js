import { createDockView } from "../js/dockview-utils.js";
import Data from "../js/data.js";
import EventHandler from "../js/event-handler.js";

export async function init(id, invoke, options) {
  console.log('DockView init called with id:', id);
  
  const el = document.getElementById(id);
  console.log('Element lookup result:', el);
  
  if (!el) {
    console.error(`Element with id '${id}' not found`);
    return null;
  }

  const dockview = createDockView(el, options);

  dockview.on("initialized", () => {
    invoke.invokeMethodAsync(options.initializedCallback);
  });
  dockview.on("lockChanged", ({ title, isLock }) => {
    invoke.invokeMethodAsync(options.lockChangedCallback, title, isLock);
  });
  dockview.on("panelVisibleChanged", ({ title, status }) => {
    invoke.invokeMethodAsync(
      options.panelVisibleChangedCallback,
      title,
      status,
    );
  });
  dockview.on("groupSizeChanged", () => {
    invoke.invokeMethodAsync(options.splitterCallback);
  });

  // Store the dockview instance in Data storage
  Data.set(id, { dockview, invoke, options });

  // Return an object reference for JSInterop
  return {
    update: (newOptions) => update(id, newOptions),
    reset: (newOptions) => reset(id, newOptions),
    save: () => save(id),
    dispose: () => dispose(id)
  };
}

export function update(id, options) {
  const dock = Data.get(id);
  if (dock) {
    const { dockview } = dock;
    dockview.update(options);
  }
}

export function reset(id, options) {
  const dock = Data.get(id);
  if (dock) {
    const { dockview } = dock;
    dockview.reset(options);
  }
}

export function save(id) {
  let ret = "";
  const dock = Data.get(id);
  if (dock) {
    const { dockview } = dock;
    ret = JSON.stringify(dockview.toJSON());
  }
  return ret;
}

export function dispose(id) {
  const dock = Data.get(id);
  Data.remove(id);

  if (dock) {
    const { dockview } = dock;
    if (dockview) {
      dockview.dispose();
    }
  }
}
