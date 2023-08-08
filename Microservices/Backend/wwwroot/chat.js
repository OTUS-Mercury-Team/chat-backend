document.addEventListener("DOMContentLoaded", () => {
    // <snippet_Connection>
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub", { accessTokenFactory: () => this.loginToken })
        .configureLogging(signalR.LogLevel.Information)
        .build();
        
    // </snippet_Connection>

    // <snippet_ReceiveMessage>
    connection.on("ReceiveMessage", (user, message) => {
        const li = document.createElement("li");
        li.textContent = `${user}: ${message}`;
        document.getElementById("messageList").appendChild(li);
    });
    // </snippet_ReceiveMessage>

    const myArrayJWT = ['eyJhbGciOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwibmJmIjoxNjkxNDk4OTgxLCJleHAiOjE2OTE0OTk1ODEsImlzcyI6IkF1dG9yaXp0aW9uTWVyY3VyeSIsImF1ZCI6IkNoYXRNZXJjdXJ5In0.iKLj-pB3zvnXzRh10bLvdqMSdk95byWG_r91qGPSwHs',
        'eyJhbGciOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU2VyZyIsIm5iZiI6MTY5MTQ5OTEzOCwiZXhwIjoxNjkxNDk5NzM4LCJpc3MiOiJBdXRvcml6dGlvbk1lcmN1cnkiLCJhdWQiOiJDaGF0TWVyY3VyeSJ9.6-2XuvLyBL_P7b0JTyKTvykrbj0hd4SUttYprABoeHQ',
    ];//'eyJhbGciOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTWF4IiwibmJmIjoxNjkxNDk0OTQ3LCJleHAiOjE2OTE0OTU1NDcsImlzcyI6IkF1dG9yaXp0aW9uTWVyY3VyeSIsImF1ZCI6IkNoYXRNZXJjdXJ5In0.4rHCKZWL_HrACYmtkl-YuuUVMhozuBWlxFWolDgBXYg',
        //'eyJhbGciOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWxleCIsIm5iZiI6MTY5MTQ5NDk2MiwiZXhwIjoxNjkxNDk1NTYyLCJpc3MiOiJBdXRvcml6dGlvbk1lcmN1cnkiLCJhdWQiOiJDaGF0TWVyY3VyeSJ9.ZrF355HyoqeVeF5N2o-1IHJa9-5_S_ABYrgMEb3JsOc'];
    const myArrayName = ['string',
        'Serg'];
        //'Max',
        //'Alex'];



    const myArrayChatid = ['1', '2', '3'];

    document.getElementById("send").addEventListener("click", async () => {
        const user = document.getElementById("userInput").value;
        const message = document.getElementById("messageInput").value;
        const randomIndex = Math.floor(Math.random() * myArrayJWT.length);
        const randomIndexChat = Math.floor(Math.random() * myArrayChatid.length);
        // <snippet_Invoke>
        try {
            await connection.invoke("SendMessage", myArrayName[randomIndex], message, myArrayJWT[randomIndex], myArrayChatid[randomIndexChat]);
        } catch (err) {
            console.error(err);
        }
        // </snippet_Invoke>
    });

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();
});
