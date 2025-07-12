import Data from "./data.js";

const getIcon = (name, hasTitle = true) => {
  const icons = getIcons();

  let icon = null;
  const control = icons.find((i) => i.name === name);
  if (control && control.icon) {
    icon = control.icon.cloneNode(true);
    if (!hasTitle) {
      icon.removeAttribute("title");
    }
  }
  // Return null if icon not found - caller should handle gracefully
  return icon;
};

const getIcons = () => {
  let icons = Data.get("bdziam-dockview");
  if (icons === null) {
    icons = [
      "bar",
      "dropdown",
      "pin",
      "pushpin",
      "lock",
      "lock_open",
      "down",
      "full",
      "restore",
      "float",
      "dock",
      "close",
      "dropdown",
    ].map((v) => {
      // Try multiple selectors to find the icon element
      let iconElement = document.querySelector(
        `template > .b-dockview-control-icon-${v}`,
      ) || document.querySelector(
        `template [data-function="${v}"]`,
      ) || document.querySelector(
        `template [title="${v}"]`,
      );
      
      return {
        name: v,
        icon: iconElement, // Can be null if not found
      };
    });
    Data.set("bdziam-dockview", icons);
  }
  return icons;
};



export { getIcons, getIcon };
