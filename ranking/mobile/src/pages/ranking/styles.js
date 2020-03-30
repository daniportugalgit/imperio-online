import { StyleSheet } from 'react-native'
import Constants from 'expo-constants'

export default StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#FFF",
        paddingHorizontal: 24,
        paddingTop: Constants.statusBarHeight + 20
    },

    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center'
    },

    filterButton: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        backgroundColor: "#008800",
        borderRadius: 8,
        width: '40%',
        justifyContent: 'center',
        alignItems: 'center',
        height: 30,
        marginBottom: 20
    },

    filterButtonText: {
        fontSize: 15,
        color: '#FFF',
        marginLeft: 6
    },

    logo: {
        width: 40,
        height: 40,
        top: -10
    }
})