﻿window.copyToClipboard = (text) => {
    navigator.clipboard.writeText(text).then(() => {
        console.log("Copied to clipboard: ", text);
    }).catch(err => {
        console.error("Could not copy text: ", err);
    });
}