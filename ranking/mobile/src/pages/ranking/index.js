import React from 'react'
import { Feather } from '@expo/vector-icons'
import { View, Text, TouchableOpacity } from 'react-native'
import { useNavigation, useRoute } from '@react-navigation/native'
import Leaderboard from 'react-native-leaderboard'

//import logoImg from '../../assets/logo.png'
import styles from './styles'



export default function Ranking() {
    const navigation = useNavigation();
    const route = useRoute();

    const state = {
        data: [
            {userName: 'Alice', highScore: 52},
            {userName: 'Bob', highScore: 120},   
            {userName: 'Carlos', highScore: 125},   
            {userName: 'Daniel', highScore: 151},   
            {userName: 'Eder', highScore: 110},   
        ]
    }

    function navigateBack() {
        navigation.goBack();
    }

    return (
        <View style={styles.container}>
            <Leaderboard 
                data={state.data} 
                sortBy='highScore' 
                labelBy='userName'/>
        </View>
    )

}
