const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:64862';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/auth/sign-in",
      "/test",
      "/tests",
      "/test/check",
      "/auth/hash-password",
      "/swagger/index.html",
      "/swagger/v1/swagger.json",
      "/swagger/swagger-ui.css",
      "/_framework/aspnetcore-browser-refresh.js",
      "/swagger/swagger-ui-bundle.js",
      "/swagger/swagger-ui-standalone-preset.js",
      "/_vs/browserLink",
      "/_framework/aspnetcore-browser-refresh.js"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
