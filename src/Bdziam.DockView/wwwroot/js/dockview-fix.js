const fixObject = data => {
    // Ensure we have a valid structure
    if (!data) {
        return {
            grid: {
                root: {
                    type: 'branch',
                    data: []
                }
            },
            panels: {},
            activeGroup: null
        };
    }

    // Ensure grid exists with proper structure
    if (!data.grid) {
        data.grid = {
            root: {
                type: 'branch',
                data: []
            }
        };
    }

    // Ensure root is proper branch type
    if (!data.grid.root || data.grid.root.type !== 'branch') {
        data.grid.root = {
            type: 'branch',
            data: data.grid.root?.data || []
        };
    }

    // Fix floating groups positioning
    data.floatingGroups?.forEach(item => {
        let { width, height } = item.position
        item.position.width = width - 2
        item.position.height = height - 2
    });

    // Remove invisible branches
    if (data.grid.root) {
        removeInvisibleBranch(data.grid.root);
    }
    
    return data;
}

const removeInvisibleBranch = branch => {
    if (branch.type === 'leaf') {
        if (branch.visible === false) {
            delete branch.visible
        }
    }
    else {
        // Handle case where branch.data might not be an array
        if (branch.data && Array.isArray(branch.data)) {
            branch.data.forEach(item => {
                removeInvisibleBranch(item)
            })
        } else if (branch.data) {
            // If it's an object, try to process it directly
            removeInvisibleBranch(branch.data)
        }
    }
}

export { fixObject };
