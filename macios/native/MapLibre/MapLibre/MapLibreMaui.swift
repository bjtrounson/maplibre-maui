//
//  DotnetNewBinding.swift
//  NewBinding
//
//  Created by .NET MAUI team on 6/18/24.
//

import Foundation

@objc(MapLibreMaui)
public class MapLibreMaui : NSObject
{

    @objc
    public static func getString(myString: String) -> String {
        return myString  + " from swift!"
    }

}
