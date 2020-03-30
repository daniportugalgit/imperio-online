import React, { useState, useEffect } from 'react'
import { Feather } from '@expo/vector-icons'
import { View, Text, TouchableOpacity } from 'react-native'
import { useNavigation, useRoute } from '@react-navigation/native'
import Leaderboard from 'react-native-leaderboard'

import api from '../../services/api'

//import logoImg from '../../assets/logo.png'
import styles from './styles'

/*
    [
        {name: 'Alice', points: 52, victories: 2},
        {name: 'Bob', points: 120, victories: 7},
        {name: 'Carlos', points: 125, victories: 3},
        {name: 'Daniel', points: 151, victories: 2},
        {name: 'Eder', points: 110, victories: 1}
    ]
*/


export default function Ranking() {
    const navigation = useNavigation();
    const route = useRoute();

    const [loading, setLoading] = useState(false)
    const [personasCount, setPersonasCount] = useState(0)
    const [filter, setFilter] = useState("victories")
    const [personas, setPersonas] = useState([])

    async function loadRanking() {
        if(loading)
            return

        if(personasCount > 0 && personas.length == personasCount)
            return;

        setLoading(true)

        const response = await api.get('torque/ranking')
        setPersonas(response.data)
        setPersonasCount(personas.length)

        setLoading(false)
    }

    function forceReloadData() {
        setLoaded(false)
        loadRanking()
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
