function getPatentByWins(wins) {
    const humano = [
        { name: "Recruta", minWins: 0, maxWins: 0 },
        { name: "Cadete", minWins: 1, maxWins: 9 },
        { name: "Aspirante", minWins: 10, maxWins: 24 },
        { name: "Sargento", minWins: 25, maxWins: 49 },
        { name: "Tenente", minWins: 50, maxWins: 99 },
        { name: "Capitão", minWins: 100, maxWins: 149 },
        { name: "Major", minWins: 150, maxWins: 199 },
        { name: "Coronel", minWins: 200, maxWins: 299 },
        { name: "General", minWins: 300, maxWins: 399 },
        { name: "Marechal", minWins: 400, maxWins: 499 },
        { name: "Imperador", minWins: 500, maxWins: 649 },
        { name: "Imortal", minWins: 650, maxWins: 799 },
        { name: "Lendário", minWins: 800, maxWins: 999 },
        { name: "Titã", minWins: 1000, maxWins: 99999 }
    ]

    for (let i = 0; i < humano.length; i++) {
        const element = humano[i]
        if(wins >= element.minWins && wins <= element.maxWins)
            return element.name
    }  
}

export { getPatentByWins }