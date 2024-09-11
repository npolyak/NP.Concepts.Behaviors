using RxPropChangeObserverSample;

Person person = new Person() { Name = "Person1" };

//var observable = 
//    Observable.FromEvent<PropertyChangedEventHandler, (object? p, PropertyChangedEventArgs e)>
//(
//    h1 => (p1, e1) => h1((p1, e1)),
//    h => person.PropertyChanged += h,
//    h => person.PropertyChanged -= h
//);

//void OnEventFired((object p, PropertyChangedEventArgs e) tuple)
//{
//    Console.WriteLine((tuple.p as Person).Name);
//}

person.PropChangedToObservable(nameof(Person.Name), p => p.Name).Subscribe(OnPropertyChanged);

void OnPropertyChanged(PropChangedInfo<Person, string> info)
{
    Console.WriteLine(info.PropValue);
}


person.Name = "Person2";
person.Name = "Person3";