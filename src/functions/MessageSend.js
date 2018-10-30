module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
    context.log(req)

    var request = require('request');

    var data = encodeURI(req.query.data)
    var tel = req.query.tel

    if (tel == null)
    {
        tel = '5519981411694'
    }

    context.log('data: ' + data);

    var options = {
        url: 'https://panel.apiwha.com/send_message.php?apikey=QM7TA4U3ORNUY7JP9CLD&number=' + tel + '&text=' + data
    };

    function callback(error, response, body) {
        if (!error && response.statusCode == 200) {
            context.log(body);
            request.post('https://eudora-09-api.azurewebsites.net/api/mensagens', {form:{From:"555196640471", To:"5519981411694", Content:data, Sentiment:"-1"}})
        } else {
            context.log(error || body);
        }
    }

    request(options, callback);

}