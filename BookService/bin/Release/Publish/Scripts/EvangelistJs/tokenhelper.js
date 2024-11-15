function setWithExpiry(key, value, ttl) {
    const now = new Date()

    // `item` is an object which contains the original value
    // as well as the time when it's supposed to expire
    const item = {
        value: value,
        expiry: now.getTime() + ttl
    }
    localStorage.setItem(key, JSON.stringify(item))
}

function getWithExpiry(key) {
    const itemStr = localStorage.getItem(key)

    // if the item doesn't exist, return null
    if (!itemStr) {
        return null
    }

    const item = JSON.parse(itemStr)
    const now = new Date()

    // compare the expiry time of the item with the current time
    if (now.getTime() > item.expiry) {
        // If the item is expired, delete the item from storage
        // and return null
        localStorage.removeItem(key)
        return null
    }
    return item.value
}

function getAccessToken() {
    console.log("getAccessToken 1");
    const tokenKey = "AUTHTOKEN";
    var accessToken = getWithExpiry(tokenKey);
    if (accessToken != null) return accessToken;
    console.log("getAccessToken 2");
    var body = {
        grant_type: 'password',
        //client_id: 'myClientId',
        //client_secret: 'myClientSecret',
        username: loginusername,
        password: loginpassword
    };

    $.ajax({
        url: '/token',
        type: 'POST',
        dataType: 'json',
        async: false,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: body, /* right */
        complete: function (result) {
            console.log(result);
        },

        success: function (result) {
            console.log(result.access_token);
            var token = "Bearer " + result.access_token;
            setWithExpiry(tokenKey, token, 3600000);
            console.log("getAccessToken 3");
            console.log(result.access_token);
            accessToken = token;
        },

    });
    return accessToken;
}











