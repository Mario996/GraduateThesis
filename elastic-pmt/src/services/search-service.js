async function search (query) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            query,
        }),
    }

    return fetch('https://localhost:44332/priorities/search', requestOptions)
        .then((response) => response.json())
}

export const searchService = {
    search,
}
