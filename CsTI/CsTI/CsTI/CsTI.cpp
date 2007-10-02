#include "stdafx.h"

#include "CsTI.h"

#include "Feedback.h"

//this is a singleton class
namespace TouchlibWrapper
{

	CsTI* CsTI::Instance()
	{
		if (_instance == 0)
		{  
		  _instance = new CsTI;
		}
	
		return _instance;
	  
	}


	//protected constructor
	CsTI::CsTI()
	{
		std::cout << "RAN CONSTRUCTOR" << std::endl;
	}

	//standard destructor
	CsTI::~CsTI()
	{
		delete m_pUnmanaged;
		delete m_pFeedback;
	}

	void CsTI::startScreen()
	{
		m_pFeedback = new Feedback( this );
		m_pUnmanaged = new CTouchlibWrapper( m_pFeedback );
	}

	//fingerDown
	//this part adds/removes/raises the events for .net
	void CsTI::add_fingerDown( fingerDownHandler* pfingerDown )
	{
		m_pFingerDown = static_cast< fingerDownHandler* >(Delegate::Combine( m_pFingerDown, pfingerDown ));
	}

	void CsTI::remove_fingerDown( fingerDownHandler* pfingerDown )
	{
		m_pFingerDown = static_cast< fingerDownHandler* >(Delegate::Remove( m_pFingerDown, pfingerDown ));
	}

	void CsTI::raise_fingerDown( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY )
	{
		if( m_pFingerDown != 0 )
			m_pFingerDown->Invoke( ID, tagID, X, Y , angle, area, height, width, dX, dY );
	}
	//-------end fingerDown--------//

	//fingerUp
	//this part adds/removes/raises the events for .net
	void CsTI::add_fingerUp( fingerUpHandler* pfingerUp )
	{
		m_pFingerUp = static_cast< fingerUpHandler* > (Delegate::Combine( m_pFingerUp, pfingerUp ));
	}

	void CsTI::remove_fingerUp( fingerUpHandler* pfingerUp )
	{
		m_pFingerUp = static_cast< fingerUpHandler* > (Delegate::Remove( m_pFingerUp, pfingerUp ));
	}

	void CsTI::raise_fingerUp( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY )
	{
		if( m_pFingerUp != 0 )
			m_pFingerUp->Invoke( ID, tagID, X, Y , angle, area, height, width, dX, dY);
	}
	//-------end fingerDown--------//


	//fingerUpdate
	//this part adds/removes/raises the events for .net
	void CsTI::add_fingerUpdate( fingerUpdateHandler* pfingerUpdate )
	{
		m_pFingerUpdate = static_cast< fingerUpdateHandler* > (Delegate::Combine( m_pFingerUpdate, pfingerUpdate ));
	}

	void CsTI::remove_fingerUpdate( fingerUpdateHandler* pfingerUpdate )
	{
		m_pFingerUpdate = static_cast< fingerUpdateHandler* > (Delegate::Remove( m_pFingerUpdate, pfingerUpdate ));
	}

	void CsTI::raise_fingerUpdate( int ID, int tagID, float X, float Y , float angle, float area, float height, float width, float dX, float dY )
	{
		if( m_pFingerUpdate != 0 )
			m_pFingerUpdate->Invoke( ID, tagID, X, Y , angle, area, height, width, dX, dY );
	}
	//-------end fingerDown--------//

}
