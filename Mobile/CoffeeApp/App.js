import { View, Text } from 'react-native'
import React from 'react'
import { PaperProvider } from 'react-native-paper'
import Navigation from './navigation/Navigation'
import { Provider } from 'react-redux'
import store from './redux/store'
import Toast from 'react-native-toast-message'

const App = () => {
  return (
    <PaperProvider>
      <Provider store={store}>
          <Navigation />
          <Toast />
      </Provider>
    </PaperProvider>
  )
}

export default App