import React, { useState, useEffect } from 'react'
import { Feather } from '@expo/vector-icons'
import { View, Text, Image, TouchableOpacity } from 'react-native'
import { useNavigation, useRoute } from '@react-navigation/native'
import { ButtonGroup } from 'react-native-elements';
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
    const [filterIndex, setFilterIndex] = useState(0)

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

    function forceReload() {
        if(loading)
            return

        setPersonasCount(0)
        setPersonas([])
        loadRanking();
    }

    function setPatents(data) {
        const newData = data.slice(0)
        
        for (let i = 0; i < data.length; i++) {
            newData[i].name = getPatentByWins(newData[i].victories) + " " + newData[i].name
            newData[i].iconUrl = "http://exchange.funpowerhouse.com/imperio-online/grad/grad" + newData[i].victories + ".png"
        }

        return newData
    }

    function changeFilterTo(index) {
        setFilterIndex(index)

        switch(index) {
            case 0: filterBy("victories"); break;
            case 1: filterBy("points"); break;
        }
    }

    //possible filters: "victories" || "points" 
    function filterBy(fieldName) {
        setFilter(fieldName)
        setMasterKey(Math.random()) //force re-render (without it, the component will no re-sort the list)
    }

    function navigateBack() {
        navigation.goBack();
    }

    function getVictoriesBtnComponent() {
        return (<View style={styles.testButtonGroup}>
            <Feather name="award" size={16} color="#FFF" />
            <Text style={styles.filterButtonText}>Vitórias</Text>
        </View>)
    }

    function getScoreBtnComponent() {
        return (<View style={styles.testButtonGroup}>
            <Feather name="star" size={16} color="#FFF" />
            <Text style={styles.filterButtonText}>Pontos</Text>
        </View>)
    }

    return (
        <View style={styles.container}>
            <View style={styles.header}>
                <TouchableOpacity onPress={forceReload}>
                    <Image source={logoImg} style={styles.logo}/>
                </TouchableOpacity>
            </View>

            <View>
                <ButtonGroup
                    onPress={(x) => changeFilterTo(x)}
                    selectedIndex={filterIndex}
                    buttons={[{element: getVictoriesBtnComponent},{element: getScoreBtnComponent}]}
                    containerStyle={styles.buttonGroup}
                    textStyle={styles.btnGroupTextStyle}
                    selectedButtonStyle={styles.selectedButtonStyle}
                    />
            </View>

            <Leaderboard 
                key={masterKey}
                data={personas} 
                sortBy={filter}
                icon="iconUrl"
                oddRowColor="#f2f5f7"
                evenRowColor="#fff"
                labelBy='name'/>
        </View>
    )

}


/*

*/