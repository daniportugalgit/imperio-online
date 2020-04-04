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

const investedResearch = newMetrics('invested_research', ['min', 'pet', 'ura', 'credits'])
const finishedResearch = newMetrics('finished_research', ['research'])
const finishedGame = newMetrics('finished_game')
const login = newMetrics('login')

module.exports = { login, finishedResearch, finishedGame, middleware };
