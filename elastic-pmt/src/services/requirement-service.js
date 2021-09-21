async function getAllRequirements () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
        .then((response) => response.json())
}

async function addRequirementToList (dto) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            ControlClass: dto.controlClass,
            ControlId: dto.controlId,
            ControlTitle: dto.controlTitle,
            GroupTitle: dto.groupTitle,
            PartId: dto.partId,
            PartProse: dto.partProse
        }),
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
        .then((response) => response.json())
}

async function deleteRequirementFromList (id) {
    const requestOptions = {
        method: 'DELETE',
    }

    return fetch('https://localhost:44332/requirements/' + id, requestOptions)
        .then((response) => response.json())
}

export const requirementService = {
    getAllRequirements,
    addRequirementToList,
    deleteRequirementFromList
}
