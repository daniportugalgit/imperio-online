import React, { useState, useEffect } from 'react'
import { Feather } from '@expo/vector-icons'
import { View, Text, Image, TouchableOpacity } from 'react-native'
import { useNavigation, useRoute } from '@react-navigation/native'
import Leaderboard from 'react-native-leaderboard'

import api from '../../services/api'
import { getPatentByWins } from '../../models/Patents'

import logoImg from '../../../assets/icon.png'
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
    const [masterKey, setMasterKey] = useState(0)
    const [personas, setPersonas] = useState([])

    async function loadRanking() {
        if(loading)
            return

        if(personasCount > 0 && personas.length == personasCount)
            return;

        setLoading(true)

        const response = await api.get('api/ranking')
        const personasWithPatents = setPatents(response.data)
    
        setPersonas(personasWithPatents)
        setPersonasCount(personasWithPatents.length)
        

        setLoading(false)
    }

    useEffect(() => {
        loadRanking()
    })

    function setPatents(data) {
        const newData = data.slice(0)
        
        for (let i = 0; i < data.length; i++) {
            newData[i].name = getPatentByWins(newData[i].victories) + " " + newData[i].name
        }

        return newData
    }

    //possible filters: "victories" || "points" 
    function filterBy(fieldName) {
        setFilter(fieldName)
        setMasterKey(Math.random()) //force re-render (without it, the component will no re-sort the list)
    }

    function navigateBack() {
        navigation.goBack();
    }

    return (
        <View style={styles.container}>
            <View style={styles.header}>
                <TouchableOpacity
                    style={styles.filterButton}
                    onPress={() => filterBy("victories")}>

                    <Feather name="award" size={16} color="#FFF" />
                    <Text style={styles.filterButtonText}>Vitórias</Text>
                </TouchableOpacity>

                <Image source={logoImg} style={styles.logo}/>

                <TouchableOpacity
                    style={styles.filterButton}
                    onPress={() => filterBy("points")}>

                    <Feather name="star" size={16} color="#FFF" />
                    <Text style={styles.filterButtonText}>Pontos</Text>
                </TouchableOpacity>
            </View>
            <Leaderboard 
                key={masterKey}
                data={personas} 
                sortBy={filter}
                labelBy='name'/>
        </View>
    )

}
