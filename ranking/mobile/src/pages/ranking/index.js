import React, { useState, useEffect } from 'react'
import { Feather } from '@expo/vector-icons'
import { View, Text, TouchableOpacity } from 'react-native'
import { useNavigation, useRoute } from '@react-navigation/native'
import Leaderboard from 'react-native-leaderboard'

import api from '../../services/api'

//import logoImg from '../../assets/logo.png'
import styles from './styles'



export default function Ranking() {
    const navigation = useNavigation();
    const route = useRoute();

    const [loading, setLoading] = useState(false)
    const [filter, setFilter] = useState("victories")
    const [personas, setPersonas] = useState([
        {name: 'Alice', points: 52, victories: 2},
        {name: 'Bob', points: 120, victories: 7},
        {name: 'Carlos', points: 125, victories: 3},
        {name: 'Daniel', points: 151, victories: 2},
        {name: 'Eder', points: 110, victories: 1}
    ])

    async function loadRanking() {
        if(loading)
            return

        //todo: revisar se precisa de mais alguma verificação aqui, de acordo com o fluxo final do app

        setLoading(true)

        const response = await api.get('ranking')
        setPersonas([...personas, ...response.data])

        setLoading(false)
    }

    useEffect(() => {
        loadRanking()
    })

    function navigateBack() {
        navigation.goBack();
    }

    //possible filters: "victories" || "points" 
    function filterBy(fieldName) {
        setFilter(fieldName)
    }

    return (
        <View style={styles.container}>
            <Leaderboard 
                data={personas} 
                sortBy={filter}
                labelBy='name'/>
        </View>
    )

}
