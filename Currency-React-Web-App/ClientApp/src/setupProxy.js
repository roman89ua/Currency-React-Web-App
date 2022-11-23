const {createProxyMiddleware} = require('http-proxy-middleware');
const {env} = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:63188';

const context = [
  "/home",
  "/home/updatedbonappatart",
  "/weatherforecast",
  "/currencycurrentdate",
  "/currencycurrentdate/sortcurrencydata/key/order",
  "/currencycurrentdate/filtercurrencydata/value",
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
