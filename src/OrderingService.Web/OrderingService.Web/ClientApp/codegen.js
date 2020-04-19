const http = require("http");
const fs = require('fs');
var CodeGen = require("swagger-typescript-codegen").CodeGen;

const swaggerJSON = 'http://localhost:5000/swagger/v1/swagger.json';
http.get(swaggerJSON, (res) => {
    if (res.statusCode !== 200) {
        console.error(new Error('Request Failed.\n' + `Status Code: ${res.statusCode}`));
        res.resume();
        return;
    }

    res.setEncoding('utf8');
    let rawData = '';
    res.on('data', (chunk) => {
        rawData += chunk;
    });
    res.on('end', () => {
        try {
            const parsedData = JSON.parse(rawData);

            var tsSourceCode = CodeGen.getTypescriptCode({
                className: "SwaggerCodegen",
                swagger: parsedData,
                lint: false,
                esnext: true,
                beautify: true,
              });

            fs.writeFileSync('src/WebApiModels.ts', tsSourceCode, {
                encoding: 'utf8'
            });
            console.info('Models successfully generated.');
        } catch (e) {
            console.error(e.message);
        }
    });
}).on('error', (e) => {
    console.error(`Got error: ${e.message}`);
});