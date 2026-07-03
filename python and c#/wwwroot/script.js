function loadSharedHeader() {
   let Header = document.getElementById('shared-header');
    if (!Header) {
        return Promise.resolve();
    }

    return fetch('/header.html')
        .then(function (response) {
            return response.text();
        })
        .then(function (html) {
            Header.innerHTML = html;
        })
      .catch (function (error) {
        Header.innerHTML = "<p>Header failed to load</p>";
    });
    
}
let pvotes = 0;
let cvotes = 0;
let url = "";
function resetvote() {
    fetch('/api/dv', { method: 'POST' })
        .then(function () {
            window.location.href = "vote.html";
        })
        .catch(function (error) {
            console.log('Error resetting vote:', error);
        });
}
function castp() {
    fetch('/api/python', { method: 'POST' })
        .then(function () {
            votep();
        })
        .catch(function (error) {
            console.log('Error casting vote:', error);
        });
}

function castc() {
    fetch('/api/cs', { method: 'POST' })
        .then(function () {
            votec();
        })
        .catch(function (error) {
            console.log('Error casting vote:', error);
        });
}

function votep() {
    const vote = document.getElementById("pn");
    fetch('/api/python')
        .then(function (response) {
            return response.text();
        })
        .then(function (count) {
            let pvotes = count;
            vote.innerHTML = "python Votes: " + pvotes;
        })
        .catch(function (error) {
            console.log('Error fetching votes:', error);
        });
}

function votec() {
    const vote = document.getElementById("cn");
    fetch('/api/cs')
        .then(function (response) {
            return response.text();
        })
        .then(function (count) {
            let cvotes = count;
            vote.innerHTML = "c# Votes: " + cvotes;
        })
        .catch(function (error) {
            console.log('Error fetching votes:', error);
        });
}
function setp(x) {
    switch (x) {
        case 0:
            url = "home.html";
            break;
        case 1:
            url = "python.html";
            break;
        case 2:
            url = "csharp.html";
            break;
        case 3:
            url = "vote.html";
            break;
    }
    window.location.href = url;
}
