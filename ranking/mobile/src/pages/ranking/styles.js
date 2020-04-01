import { StyleSheet } from 'react-native'
import Constants from 'expo-constants'

export default StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#020",
        paddingHorizontal: 24,
        paddingTop: Constants.statusBarHeight + 20
    },

    header: {
        flexDirection: 'row',
        justifyContent: 'center',
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
        width: 80,
        height: 80,
        top: -10
    },

    buttonGroup: {
        flexDirection: 'row',
        justifyContent: 'center',
        backgroundColor: "#888",
        borderRadius: 8,
        alignItems: 'center',
        height: 34,
        width: '100%',
        marginTop: 10,
        marginBottom: 10,
        marginLeft:0
    },

    testButtonGroup: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
    },

    selectedButtonStyle: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        backgroundColor: "#008800",
        borderRadius: 8,
        justifyContent: 'center',
        alignItems: 'center',
    },

    btnGroupTextStyle: {
        color: '#ddd'
    }
})