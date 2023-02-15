import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import React from 'react';
import DistillationHome from './screens/DistillationHomeScreen';
import MaterialSelection from './screens/MaterialSelection';
import ProductInfo from './screens/ProductInfo';
import RunLog from './screens/RunLog';
import Reports from './screens/Reports';

const Stack = createNativeStackNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="DistillationHome" component={DistillationHome} />
        <Stack.Screen name="Material Selection" component={MaterialSelection} />
        <Stack.Screen name="Product Information" component={ProductInfo} />
        <Stack.Screen name="Run Log" component={RunLog} />
        <Stack.Screen name="Reports" component={Reports} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default App;
