import * as React from 'react';
import {NavigationContainer, Tab} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import RunLog from './RunLog';
import HomeScreen from './HomeScreen';
import MaterialChoice from './MaterialChoice';
import RawMaterial from './RawMaterial';
import Sample from './Sample';
import Reports from './Reports';

const Stack = createNativeStackNavigator();

export default class App extends React.Component {
  render() {
    return (
      <NavigationContainer>
        <Stack.Navigator>
          <Stack.Screen name="Home" component={HomeScreen} />
          <Stack.Screen name="MaterialChoice" component={MaterialChoice} />
          <Stack.Group>
            <Stack.Screen name="Raw Material" component={RawMaterial} />
            <Stack.Screen name="Run Log" component={RunLog} />
            <Stack.Screen name="Sample" component={Sample} />
          </Stack.Group>
          <Stack.Screen name="Reports" component={Reports} />
        </Stack.Navigator>
      </NavigationContainer>
    );
  }
}
