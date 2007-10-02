/*
 *  This is a the main part of CsTI.
 *  Check the CsTI_README.txt for exact instructions on using it.
 * 
 * 
 */

#pragma once
//please change the references for your own computer
//TODO: make relative paths
#include "C:\CsTI\TouchlibWrapper\TouchlibWrapper\TouchlibWrapper.h"
#include "TouchData.h" //don't think this is needed anymore?

class Feedback;

using namespace System;

namespace TouchlibWrapper {

	//sends the event to relevant handler
	public __delegate void fingerDownHandler( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );
	public __delegate void fingerUpHandler( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );
	public __delegate void fingerUpdateHandler( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );

	//this is now a singleton class, for safety and health reasons
	public __gc class CsTI
	{
		public:
			//gets the singleton instance
			static CsTI* Instance();
			
			

			//running this will start the touchscreen
			void startScreen();

			//add/remove fingerdown event handler
			__event void add_fingerDown( fingerDownHandler* pfingerDown );
			__event void remove_fingerDown( fingerDownHandler* pfingerDown );

			//add/remove fingerup event handler
			__event void add_fingerUp( fingerUpHandler* pfingerUp );
			__event void remove_fingerUp( fingerUpHandler* pfingerUp );

			//add/remove fingerupdate event handler
			__event void add_fingerUpdate( fingerUpdateHandler* pfingerUpdate );
			__event void remove_fingerUpdate( fingerUpdateHandler* pfingerUpdate );

			//this part raises the events
		private public:
			__event void raise_fingerDown( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );
			__event void raise_fingerUp( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );
			__event void raise_fingerUpdate( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY );

		private:
			CTouchlibWrapper __nogc* m_pUnmanaged;
			Feedback __nogc* m_pFeedback;
			fingerDownHandler* m_pFingerDown;
			fingerUpHandler* m_pFingerUp;
			fingerUpdateHandler* m_pFingerUpdate;

			static CsTI* _instance = 0;

			//singleton constructor/destructor
		protected:
			CsTI();
			~CsTI();
			
	};
}
