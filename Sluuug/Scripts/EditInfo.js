function newEvent() {
}

function newEducation() {
}

function newWork() {
}

function newLivePlace() {
}

function dropEntry(entry) {
    let EntryId = entry.id;
    console.log(EntryId);
    $.post("/api/drop_entry",
        {EntryId},
        function (result) {
            console.log(result);
            if (result == true) {
                document.location.reload();
            }
        })
}