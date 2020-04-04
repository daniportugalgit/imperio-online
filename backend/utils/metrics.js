const promBundle = require('express-prom-bundle')

const middleware = promBundle({
  includeMethod: true,
  collectDefaultMetrics: {
    timeout: 5000,
  },
})


const newMetrics = (name, labelNames) => {
  return new middleware.promClient.Counter({
    name: name,
    help: 'help',
    labelNames: labelNames
  })
}

const investResearch = newMetrics('imperio_invest_research', ['resource'])
const endResearch = newMetrics('imperio_end_research', ['research'])
const endGame = newMetrics('imperio_end_game')
const login = newMetrics('imperio_login')

module.exports = { login, investResearch, endResearch, endGame, middleware };
