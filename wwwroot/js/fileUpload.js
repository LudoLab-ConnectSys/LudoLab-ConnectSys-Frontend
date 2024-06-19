window.getFile = async (input) => {
    const file = input.files[0];
    if (!file) {
        return null;
    }

    return {
        name: file.name,
        size: file.size,
        type: file.type,
        content: new Uint8Array(await file.arrayBuffer())
    };
};
