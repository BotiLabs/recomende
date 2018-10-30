
var qs = require('querystring')
var http = require('http')
var request = require('request');

module.exports = function(context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
    context.log(req.body);
    var data = req.body
    var parsed = qs.parse(data)

    var json = JSON.parse(parsed.data)
    var from = json.from
    var to = json.to
    var text = json.text

    let accessKey = 'b9c5c4ae228d409d8de36578d62db9be';
    let uri = 'westus.api.cognitive.microsoft.com';
    let path = '/text/analytics/v2.0/sentiment';

    const url = 'https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment';
    const textanalyticskey = 'b9c5c4ae228d409d8de36578d62db9be';

    request.post(url, {
            json: {
                'documents': [{'id': '1', 'language': 'pt', 'text': text }]
            },
            "headers": {
                'Ocp-Apim-Subscription-Key': textanalyticskey
            }
        }, sentimentCallback);
        
    function sentimentCallback(error, response, body) {
        context.log("sentimentCallback")
        
        var sentiment = -1
        
        if (!error && response.statusCode == 200) {
            sentiment = parseFloat(body.documents[0].score).toFixed(2)
            context.log("sentiment " + sentiment)
        } else {
            context.log(error || body);
        }
        
        request.post('https://eudora-09-api.azurewebsites.net/api/mensagens', {form:{From:from, To:to, Content:text, Sentiment:sentiment}}, addToDatabaseCallback)
    }

    function addToDatabaseCallback(error, response, body) {
        context.log("addToDatabaseCallback")
        
        if (!error && response.statusCode == 200) {
            context.log("addToDatabaseCallback success")
        } else {
            context.log(error || body);
        }
    }
};