const models = require('../models')

module.exports = () => {
    const result = models.sequelize.sync({ force: true })
  
    return result.then(x => {
        setTimeout(() => { process.exit(0) }, 2000);
        return 'Success';
    });
}

require('make-runnable')