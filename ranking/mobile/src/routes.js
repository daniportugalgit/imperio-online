import React from 'react'
import { NavigationContainer } from '@react-navigation/native'
import { createStackNavigator, TransitionPresets } from '@react-navigation/stack'

const AppStack = createStackNavigator();

import Ranking from './pages/ranking'

export default function Routes() {
    return (
        <NavigationContainer>
            <AppStack.Navigator screenOptions={{ headerShown:false, ...TransitionPresets.SlideFromRightIOS }}>

                <AppStack.Screen name="Ranking" component={Ranking} />

            </AppStack.Navigator>
        </NavigationContainer>
    )
}