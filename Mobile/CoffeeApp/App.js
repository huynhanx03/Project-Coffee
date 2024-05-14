import 'react-native-gesture-handler'
import { View, Text } from 'react-native'
import React from 'react'
import { PaperProvider } from 'react-native-paper'
import Navigation from './navigation/Navigation'
import { Provider } from 'react-redux'
import store from './redux/store'
import Toast from 'react-native-toast-message'
import { GestureHandlerRootView } from 'react-native-gesture-handler'

const App = () => {
  return (
    <GestureHandlerRootView style={{flex: 1}}>
      <PaperProvider>
        <Provider store={store}>
            <Navigation />
            <Toast />
        </Provider>
      </PaperProvider>
    </GestureHandlerRootView>
  )
}

export default App