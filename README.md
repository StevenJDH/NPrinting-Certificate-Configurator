# NPrinting Certificate Configurator
This program is used to automatically install and configure Qlik NPrinting with a CA signed certificate automatically. There are various steps needed to do this manually, including converting the certificate to the correct format with OpenSSL, but these steps and or folder locations will vary depending on the version you use. NPrinting Certificate Configurator takes care of these error-prone steps with a click of a button.

![Program Screenshot](https://github.com/StevenJDH/NPrinting-Certificate-Configurator/raw/master/ncc-ss.jpg "Screenshot")

Releases: [https://github.com/StevenJDH/NPrinting-Certificate-Configurator/releases](https://github.com/StevenJDH/NPrinting-Certificate-Configurator/releases)

Changelog: [https://github.com/StevenJDH/NPrinting-Certificate-Configurator/wiki/Changelog](https://github.com/StevenJDH/NPrinting-Certificate-Configurator/wiki/Changelog)

## Features
* Automatically convert a CA signed certificate and install it.
* Automatically update NPrinting's NewsStand and Web Console configuration to use the certificate.
* Optionally create a configuration backup before making changes.
* Restore configuration to revert back to using the default signed certificate.
* Temporarily disable the use of the CA signed certificate to use the self-signed one.
* Optionally opt to restart the Qlik Web Engine service after the auto configuration process.

## Compatibility
* Qlik NPrinting 17.0 or newer.
* 64-bit operating systems only.

## Disclaimer
NPrinting Certificate Configurator is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

## Do you have any questions?
Many commonly asked questions are answered in the FAQ:
[https://github.com/StevenJDH/NPrinting-Certificate-Configurator/wiki/FAQ](https://github.com/StevenJDH/NPrinting-Certificate-Configurator/wiki/FAQ)

## Need to contact me?
I can be reached here directly at [https://21.co/stevenjdh](https://21.co/stevenjdh "Contact Page")

## Want to show your support?

|Method       | Address                                                                                                    |
|------------:|:-----------------------------------------------------------------------------------------------------------|
|PayPal:      | [https://www.paypal.me/stevenjdh](https://www.paypal.me/stevenjdh "Steven's Paypal Page")                  |
|Bitcoin:     | 3GyeQvN6imXEHVcdwrZwKHLZNGdnXeDfw2                                                                         |
|Litecoin:    | MAJtR4ccdyUQtiiBpg9PwF2AZ6Xbk5ioLm                                                                         |
|Ethereum:    | 0xa62b53c1d49f9C481e20E5675fbffDab2Fcda82E                                                                 |
|Dash:        | Xw5bDL93fFNHe9FAGHV4hjoGfDpfwsqAAj                                                                         |
|Zcash:       | t1a2Kr3jFv8WksgPBcMZFwiYM8Hn5QCMAs5                                                                        |
|PIVX:        | DQq2qeny1TveZDcZFWwQVGdKchFGtzeieU                                                                         |
|Ripple:      | rLHzPsX6oXkzU2qL12kHCH8G8cnZv1rBJh<br />Destination Tag: 2357564055                                        |
|Monero:      | 4GdoN7NCTi8a5gZug7PrwZNKjvHFmKeV11L6pNJPgj5QNEHsN6eeX3D<br />&#8618;aAQFwZ1ufD4LYCZKArktt113W7QjWvQ7CWDXrwM8yCGgEdhV3Wt|


// Steven Jenkins De Haro ("StevenJDH" on GitHub)
