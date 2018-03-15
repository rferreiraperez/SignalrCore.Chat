
//////////////////////////////////////////////
// Chat client
//////////////////////////////////////////////

// Create hub connection
const connection = new signalR.HubConnection('http://localhost:44514/chathub/');

// Handle message from hub
connection.on('ReceiveMessage', (timestamp, user, message) => {
    const encodedUser = user;
    const encodedMsg = message;
    const listItem = document.createElement('li');
    listItem.innerHTML = timestamp + ' <b>' + encodedUser + '</b>: ' + encodedMsg;
    document.getElementById('messages').appendChild(listItem);
});

// Send message to hub
document.getElementById('sendButton').addEventListener('click', event => {
    const msg = document.getElementById('messageTextArea').value;
    const usr = document.getElementById('nicknameInputText').value;
    connection.invoke('SendMessage', usr, msg).catch(err => showErr(err));
    event.preventDefault();
});

// Show errors to the users
function showErr(msg) {
    const listItem = document.createElement('li');
    listItem.setAttribute("style", "color: red");
    listItem.innerText = msg.toString();
    document.getElementById('messages').appendChild(listItem);
}

// Start connection
connection.start().catch(err => showErr(err));